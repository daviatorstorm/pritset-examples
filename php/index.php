<?php

declare(strict_types=1);

require __DIR__ . '/vendor/autoload.php';

use GuzzleHttp\Client as HttpClient;
use Pritset\Exception\PritsetApiException;
use Pritset\Exception\PritsetTransportException;
use Pritset\PritsetClient;

function requireEnvironmentVariable(string $name): string
{
    $value = getenv($name);
    if ($value === false || trim($value) === '') {
        throw new RuntimeException("Set the {$name} environment variable before running this example.");
    }

    return trim($value);
}

/** @return array<mixed> */
function loadSampleData(): array
{
    $json = file_get_contents(__DIR__ . '/Data/dummy_data.json');
    if ($json === false) {
        throw new RuntimeException('Unable to read Data/dummy_data.json.');
    }

    $data = json_decode($json, true, 512, JSON_THROW_ON_ERROR);
    if (!is_array($data)) {
        throw new RuntimeException('Data/dummy_data.json must contain a JSON object or array.');
    }

    return $data;
}

function createPritsetClient(): PritsetClient
{
    $caBundlePath = getenv('PRITSET_CA_BUNDLE_PATH');
    $httpClient = null;

    if ($caBundlePath !== false && trim($caBundlePath) !== '') {
        $caBundlePath = trim($caBundlePath);
        if (!is_file($caBundlePath) || !is_readable($caBundlePath)) {
            throw new RuntimeException('PRITSET_CA_BUNDLE_PATH must point to a readable PEM CA bundle.');
        }

        $httpClient = new HttpClient(['verify' => $caBundlePath]);
    }

    return new PritsetClient(
        accessToken: requireEnvironmentVariable('PRITSET_ACCESS_TOKEN'),
        secret: requireEnvironmentVariable('PRITSET_SECRET'),
        timeout: 120.0,
        httpClient: $httpClient,
    );
}

function main(): void
{
    $pritset = createPritsetClient();
    $pdf = $pritset->documents()->generate(
        requireEnvironmentVariable('PRITSET_TEMPLATE_ID'),
        loadSampleData(),
    );

    try {
        $contentType = strtolower(trim(explode(';', $pdf->contentType ?? '', 2)[0]));
        if ($contentType !== 'application/pdf') {
            throw new RuntimeException(
                'Expected application/pdf but received ' . ($pdf->contentType ?? 'no content type') . '.',
            );
        }

        $content = $pdf->getContents();
    } finally {
        $pdf->stream->close();
    }

    if (!str_starts_with($content, '%PDF-')) {
        throw new RuntimeException('The response did not contain a valid PDF signature.');
    }

    $outputDirectory = __DIR__ . '/output';
    if (!is_dir($outputDirectory) && !mkdir($outputDirectory, 0777, true) && !is_dir($outputDirectory)) {
        throw new RuntimeException('Unable to create the output directory.');
    }

    $outputPath = $outputDirectory . '/generated-document.pdf';
    if (file_put_contents($outputPath, $content) === false) {
        throw new RuntimeException('Unable to write the generated PDF.');
    }

    echo 'Document generated successfully: ' . realpath($outputPath) . PHP_EOL;
}

if (realpath($_SERVER['SCRIPT_FILENAME'] ?? '') === __FILE__) {
    try {
        main();
    } catch (PritsetApiException $error) {
        fwrite(STDERR, "Pritset API request failed with HTTP {$error->statusCode}." . PHP_EOL);
        exit(1);
    } catch (PritsetTransportException $error) {
        fwrite(STDERR, $error->getMessage() . PHP_EOL);
        exit(1);
    } catch (JsonException | RuntimeException | InvalidArgumentException $error) {
        fwrite(STDERR, $error->getMessage() . PHP_EOL);
        exit(1);
    }
}
