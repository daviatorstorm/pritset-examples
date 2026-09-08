package main

import (
	"context"
	"fmt"
)

func runWebhook() error {
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
	webhookURL, err := requireEnvironmentVariable("PRITSET_WEBHOOK_URL")
	if err != nil {
		return err
	}

	job, err := client.Documents().GenerateWebhook(
		context.Background(),
		templateID,
		data,
		webhookURL,
	)
	if err != nil {
		return err
	}

	fmt.Printf("Webhook generation requested: %s\n", job.ID)
	return nil
}
