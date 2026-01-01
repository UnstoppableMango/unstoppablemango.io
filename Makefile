PULUMI ?= pulumi
NIX    ?= nix

check:
	$(NIX) flake check

.PHONY: infra
infra:
	$(PULUMI) up --cwd infra
