{
  description = "My personal website";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
    flake-parts.url = "github:hercules-ci/flake-parts";

    treefmt-nix = {
      url = "github:numtide/treefmt-nix";
      inputs.nixpkgs.follows = "nixpkgs";
    };
  };

  outputs =
    inputs@{ flake-parts, ... }:
    flake-parts.lib.mkFlake { inherit inputs; } {
      imports = [ inputs.treefmt-nix.flakeModule ];

      systems = [
        "x86_64-linux"
        "x86_64-darwin"
        "aarch64-linux"
        "aarch64-darwin"
      ];
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
