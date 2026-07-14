FROM mcr.microsoft.com/devcontainers/dotnet:10.0-noble

RUN apt-get update && \
  DEBIAN_FRONTEND=noninteractive \
  apt-get -y install --no-install-recommends \ 
  curl unzip default-jre-headless

RUN curl -o /karate.jar -L 'https://github.com/karatelabs/karate/releases/download/v2.1.0/karate-2.1.0.jar'
