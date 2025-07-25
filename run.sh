#!/bin/sh

set -a
. ./backend/.env
set +a

docker compose up --build
