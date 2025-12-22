# Docker Image

Docker images are created using dockerfiles. A sample dockerfile, from which
you can create docker images, is given at the root of the repository. This
sample dockerfile will be used in the demo section.

## Create image with dockerfile

To build a docker image, run `docker build . `  at the directory your
dockerfile is in. This searches for a dockerfile in the run directory and
creates a docker image from found dockerfile. You can give name to docker image
with `-t {image-name}`, `docker build -t {image-name} . `

## Run image in container

`docker run`, creates an instance of docker image and runs it in a container.
Adding args at the end of the run command will run those arguments, thus you
can pass commands as arguments.

`docker run {image-name} args`

## Persisting Data 

To bind a host directory to container, use `docker run` with
`-v {absolutepath}:{containerpath}`.

`docker run -v {absolutepath}:{containerpath} {image-name} args`

Changes made in this folder by the container persists, you can observe these
changes in host folder. To get the output of the container in host folder, set
the output as the container folder, which is bound to host folder.

`docker run --rm -v ${PWD}/host-output:/container-output args {-output} /container-output`

## Reaching Localhost from Container

To reach your machine's localhost from the container, when giving the url use
`host.docker.internal` instead of localhost.

`http://host.docker.internal:{port}`.

## Demo

Dockerfile at the root of this repository wraps
[web-ping](https://github.com/SeriaWei/Ping). You can create a docker
image from this dockerfile and use this cli in your container by passing 
`web-ping Web.Ping --host https://github.com/` command as argument.