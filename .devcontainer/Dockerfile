# The full list of available images can be found here https://hub.docker.com/r/microsoft/devcontainers-dotnet
# noble stands for Ubuntu 24.04 LTS (Noble Numbat)
FROM mcr.microsoft.com/devcontainers/dotnet:10.0-noble

# installing jre for Karate https://stackoverflow.com/a/44154721
# and cleanup after the installation https://github.com/devcontainers/images/blob/main/docs/TIPS.md
RUN apt-get update && \
  DEBIAN_FRONTEND=noninteractive \
  apt-get -y install --no-install-recommends \ 
  default-jre-headless && \
  apt-get autoremove -y && \
  apt-get clean -y && \
  rm -rf /var/lib/apt/lists/*

# installing Karate
# https://github.com/karatelabs/karate-examples/blob/main/docker/README.md
# released versions can be found here https://github.com/karatelabs/karate/releases
RUN curl -o /karate.jar -L 'https://github.com/karatelabs/karate/releases/download/v2.1.1/karate-2.1.1.jar'
