#!/bin/bash

# Variables
PROJECT_NAME="./Backend/AlphaBotApp/WAYFS-AlphaBot.csproj"

# Targets
all() {
    build
}

build() {
    dotnet build "$PROJECT_NAME"
}

run() {
    dotnet run --project "$PROJECT_NAME"
}

# add test here

clean() {
    dotnet clean "$PROJECT_NAME"
    rm -rf bin obj
}

# Main
case "$1" in
    all)
        all
        ;;
    build)
        build
        ;;
    run)
        run
        ;;
    clean)
        clean
        ;;
    *)
        echo "Usage: $0 {all|build|run|clean}"
        exit 1
        ;;
esac
