{
  description = "My personal website";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
    flake-parts.url = "github:hercules-ci/flake-parts";
    systems.url = "github:nix-systems/default";

    treefmt-nix = {
      url = "github:numtide/treefmt-nix";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs =
    inputs@{ flake-parts, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      systems = import inputs.systems;
      imports = [ inputs.treefmt-nix.flakeModule ];

      perSystem =
        { pkgs, ... }:
        {
          devShells.default = pkgs.mkShellNoCC {
            packages =
              with pkgs;
              [
                azure-cli
                concurrently
                dotnetCorePackages.sdk_10_0
                eslint
                fable # TODO: Not on darwin
                fantomas
                git
                gnumake
                mocha
                nil
                nodejs_22
                pulumi
                shellcheck
                tailwindcss_4
                typescript-go
                webpack-cli
              ]
              ++ (with pkgs.pulumiPackages; [
                pulumi-nodejs
              ]);
          };

          treefmt = {
            programs.nixfmt.enable = true;
          };
        };
    };
}
