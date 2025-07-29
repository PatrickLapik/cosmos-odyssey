#!/bin/sh

set -a
. ./backend/.env
set +a

if [ "$1" = "db" ]; then
    docker compose --profile "db" up
fi

if [ -z "$1" ]; then
    docker compose --profile "db" --profile "backend" up --build
fi

