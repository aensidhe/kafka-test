#!/usr/bin/env bash

set -e -x -v

docker-compose run dotnet dotnet Producer/bin/Release/netcoreapp2.0/Producer.dll kafka1:9092,kafka2:9092,kafka3:9092 test-topic
