<?php

declare(strict_types=1);

require __DIR__ . '/vendor/autoload.php';
require __DIR__ . '/index.php';

use Pritset\Exception\PritsetApiException;
use Pritset\Exception\PritsetTransportException;

try {
    $pritset = createPritsetClient();
    $job = $pritset->documents()->generateWebhook(
        templateId: requireEnvironmentVariable('PRITSET_TEMPLATE_ID'),
        data: loadSampleData(),
        webhookUrl: requireEnvironmentVariable('PRITSET_WEBHOOK_URL'),
    );
    echo "Webhook generation requested: {$job->id}" . PHP_EOL;
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
