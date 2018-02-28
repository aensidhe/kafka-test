#!/usr/bin/env bash

set -e -x -v

docker-compose run dotnet dotnet Consumer/bin/Release/netcoreapp2.0/Consumer.dll kafka1:9092,kafka2:9092,kafka3:9092 test-topic
