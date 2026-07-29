# the same 24.04 version of Ubuntu is in use in Dev Container and in GitHub Actions runners 
FROM ubuntu:24.04

# installing curl and jre for Karate https://stackoverflow.com/a/44154721
# and cleanup after the installation https://github.com/devcontainers/images/blob/main/docs/TIPS.md
RUN apt-get update && \
  DEBIAN_FRONTEND=noninteractive \
  apt-get -y install --no-install-recommends \ 
  curl default-jre-headless && \
  apt-get autoremove -y && \
  apt-get clean -y && \
  rm -rf /var/lib/apt/lists/*

# installing Karate
# https://github.com/karatelabs/karate-examples/blob/main/docker/README.md
# released versions can be found here https://github.com/karatelabs/karate/releases
RUN curl -o /karate.jar -L 'https://github.com/karatelabs/karate/releases/download/v2.1.1/karate-2.1.1.jar'

# we make our container to be executable using ENTRYPOINT check basic details here https://docs.docker.com/reference/dockerfile#entrypoint
# later we pass *.feature files folder path from outside as an argument in docker run command
# then it will be forwarded here
# comprehensive article about RUN, CMD, and ENTRYPOINT here https://www.docker.com/blog/docker-best-practices-choosing-between-run-cmd-and-entrypoint/
ENTRYPOINT ["java", "-jar", "karate.jar"]
