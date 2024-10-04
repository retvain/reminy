COMPOSE_FILE := docker-compose.yml
.PHONY: u
ifeq ($(OS),Windows_NT)
PATH_SEPARATOR := \\
else
PATH_SEPARATOR := /
endif
u:  
	docker-compose -f $(COMPOSE_FILE) \
	up -d reminy-pg && \
	dotnet run --project src$(PATH_SEPARATOR)Reminy.Core.Host -- -migrate
	
.PHONY: d
d:
	docker-compose -f $(COMPOSE_FILE) down