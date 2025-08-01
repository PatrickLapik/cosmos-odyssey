#!/bin/sh

set -a
. ./backend/.env
. ./frontend/.env
set +a

if [ "$1" = "db" ]; then
    docker compose --profile "db" up
fi

if [ "$1" = "down" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" down 
fi

if [ "$1" = "detached" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" up --build -d
fi

if [ -z "$1" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" up --build --watch
fi

