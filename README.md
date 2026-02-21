# Self-Healing .NET Pipelines Demo

This repo demonstrates **self-healing CI/CD pipelines** using [GitHub Agentic Workflows](https://github.github.com/gh-aw/setup/quick-start/) and a standard .NET 10 Web API.

## The Project

A .NET 10 Web API scaffolded with `dotnet new webapi`. It has two deliberate bugs:

1. **NullReferenceException in OrderService:** `CalculateDiscount` accesses `order.Customer.LoyaltyTier` without null-checking. Guest checkouts (where `Customer` is null) crash the tests.
2. **Wrong port in Helm chart:** The deployment template uses `containerPort: 80` and a readiness probe on port 80, but .NET 10 listens on port 8080 by default. This causes Helm to time out with a cryptic `UPGRADE FAILED: timed out waiting for the condition` error.

## How It Works

A single agentic workflow (`self-heal.md`) handles both failures:

- **Test failure:** Reads `test-output.txt`, finds the `NullReferenceException`, adds a null check, opens a draft PR.
- **Deploy failure:** Reads `k8s-debug.txt` (captured by the CI pipeline), traces the readiness probe failure to the port mismatch, fixes the Helm chart, opens a draft PR.

The deployment runs on [KinD (Kubernetes in Docker)](https://kind.sigs.k8s.io/) inside the GitHub Actions runner. No Azure, no AWS, no cloud account needed.

## Getting Started

1. Fork this repository
2. Install the [gh-aw CLI](https://github.github.com/gh-aw/setup/quick-start/)
3. Set up secrets: `gh aw secrets set ANTHROPIC_API_KEY --value "<your-key>"`
4. Compile workflows: `gh aw compile`
5. Push a commit and watch the self-healing workflow open a PR

## Blog Post

This demo accompanies the blog post: [Self-Healing Pipelines: AI Fixes Your .NET Bugs While You Sleep](https://bgener.nl/blog/ai-github-pipelines-dotnet).
