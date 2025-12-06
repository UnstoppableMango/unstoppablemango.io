PULUMI ?= pulumi

.PHONY: infra
infra:
	$(PULUMI) up --cwd infra
