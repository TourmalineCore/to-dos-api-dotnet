# to-dos-api-dotnet

This repo and its infrastructure tailored for VSCode/GitHub Codespaces Dev Container centric development experience in Docker to achieve better isolation of the environment as well as its cross-platform support out of the box. 
For instance, Visual Studio for Mac support stops at .NET 8, thus, we needed something else rather than native Visual Studios for Mac and Windows. We decided to give a shot to VSCode since we already use it intensively in other stacks.

## Prerequisites

1. Install Docker Desktop (Windows, macOS) or Docker Engine (Linux)
>Note: It seems like there is Docker Engine for Linux (https://docs.docker.com/desktop/setup/install/linux/ubuntu/).
2. **Windows Only** [Install WSL](https://learn.microsoft.com/en-us/windows/wsl/install) and **clone** this repo only to the WSL file system. [This](https://github.com/microsoft/vscode-dotnettools/issues/2714#issuecomment-3818812500) GitHub issue explains what would happen otherwise. Your Solution Explorer won't work as expected.
3. Install Visual Studio Code.
4. Install all repo's recommended extensions including [Dev Containers](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers).

## Develop inside Dev Container

Open this repo's folder in VSCode/Codespaces, it might immediately propose you to re-open it in a Dev Container or you can click on `Remote Explorer`, find plus button and choose the `Open Current Folder in Container` option and wait when it is ready.

When your Dev Container is ready, the VSCode window will be re-opened. Open a new terminal in this Dev Container which will be executing the commands under this prepared Linux container where we already have all pre-installed and pre-configured development related dependencies.

### Run E2E Tests

To run Karate E2E tests execute the following script in Terminal:
```cli
java -jar /karate.jar .
```

### Run E2E Tests in Docker Compose

To run Karate E2E tests using Docker Compose execute the following script in Terminal:
```cli
docker compose up --build --exit-code-from to-dos-api-dotnet-karate-tests
```
