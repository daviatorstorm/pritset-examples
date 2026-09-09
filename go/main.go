package main

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"mime"
	"os"
	"path/filepath"

	pritset "github.com/pritset/pritset-go-sdk"
)

func main() {
	if len(os.Args) == 2 && os.Args[1] == "--webhook" {
		if err := runWebhook(); err != nil {
			reportError(err)
			os.Exit(1)
		}
		return
	}

	if err := generatePDF(); err != nil {
		reportError(err)
		os.Exit(1)
	}
}

func generatePDF() error {
	client, err := createClient()
	if err != nil {
		return err
	}
	templateID, err := requireEnvironmentVariable("PRITSET_TEMPLATE_ID")
	if err != nil {
		return err
	}
	data, err := loadSampleData()
	if err != nil {
		return err
	}

	response, err := client.Documents().Generate(
		context.Background(),
		templateID,
		data,
	)
	if err != nil {
		return err
	}
	defer response.Body.Close()

	mediaType, _, err := mime.ParseMediaType(response.ContentType)
	if err != nil || mediaType != "application/pdf" {
		if response.ContentType == "" {
			return errors.New("expected application/pdf but received no content type")
		}
		return fmt.Errorf("expected application/pdf but received %s", response.ContentType)
	}

	content, err := io.ReadAll(response.Body)
	if err != nil {
		return fmt.Errorf("read PDF response: %w", err)
	}
	if len(content) < 5 || string(content[:5]) != "%PDF-" {
		return errors.New("the response did not contain a valid PDF signature")
	}

	outputDirectory := "output"
	if err := os.MkdirAll(outputDirectory, 0o755); err != nil {
		return fmt.Errorf("create output directory: %w", err)
	}
	outputPath := filepath.Join(outputDirectory, "generated-document.pdf")
	if err := os.WriteFile(outputPath, content, 0o644); err != nil {
		return fmt.Errorf("write generated PDF: %w", err)
	}

	absOutputPath, err := filepath.Abs(outputPath)
	if err != nil {
		return err
	}
	fmt.Printf("Document generated successfully: %s\n", absOutputPath)
	return nil
}

func createClient() (*pritset.Client, error) {
	accessToken, err := requireEnvironmentVariable("PRITSET_ACCESS_TOKEN")
	if err != nil {
		return nil, err
	}
	secret, err := requireEnvironmentVariable("PRITSET_SECRET")
	if err != nil {
		return nil, err
	}

	return pritset.NewClient(
		accessToken,
		secret,
	)
}

func requireEnvironmentVariable(name string) (string, error) {
	value := os.Getenv(name)
	if value == "" {
		return "", fmt.Errorf("Set the %s environment variable before running this example.", name)
	}
	return value, nil
}

func loadSampleData() (any, error) {
	content, err := os.ReadFile(filepath.Join("Data", "dummy_data.json"))
	if err != nil {
		return nil, errors.New("Unable to read Data/dummy_data.json.")
	}

	var data any
	if err := json.Unmarshal(content, &data); err != nil {
		return nil, errors.New("Data/dummy_data.json must contain valid JSON.")
	}
	return data, nil
}

func reportError(err error) {
	var apiError *pritset.APIError
	var transportError *pritset.TransportError
	switch {
	case errors.As(err, &apiError):
		fmt.Fprintf(os.Stderr, "Pritset API request failed with HTTP %d.\n", apiError.StatusCode)
	case errors.As(err, &transportError):
		fmt.Fprintln(os.Stderr, "The Pritset request did not complete.")
	default:
		fmt.Fprintln(os.Stderr, err)
	}
}
