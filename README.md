# ECMA2 Management Agent Template

A C# starter template for building custom **ECMA2 Management Agents** (connectors) for **Microsoft Identity Manager (MIM)** and **Forefront Identity Manager (FIM)**. The template provides a fully structured scaffold covering every major ECMA2 interface, so you only need to fill in the business logic for your target system.

---

## Overview

Microsoft Identity Manager uses **Management Agents** to synchronise identity data between a central metaverse and external systems (HR databases, cloud directories, custom APIs, etc.). The ECMA2 interface (`Microsoft.MetadirectoryServicesEx`) is the extensible way to write such connectors as .NET class libraries.

This project gives you a pre-wired partial-class structure with conditional compilation symbols that let you enable or disable individual features before you write a single line of business logic.

---

## Key Technologies

| Technology | Role |
|---|---|
| C# / .NET 4.0 | Implementation language and runtime |
| Microsoft.MetadirectoryServicesEx | MIM/FIM ECMA2 SDK – base interfaces and types |
| NLog 4.5.11 | Structured logging throughout the connector |
| MSBuild / Visual Studio | Build tooling (`.csproj` / `.sln`) |

---

## Repository Structure

```
ECMA2Template/
├── ECMA2Template.sln          # Visual Studio solution file
├── ECMA2Template.csproj       # Project file; controls build targets and feature flags
├── packages.config            # NuGet packages (NLog)
├── AssemblyInfo.cs            # Assembly metadata and versioning
│
├── capabilities.cs            # Entry point: class declaration, ECMA2 interface list,
│                              #   MA capabilities (export type, DN style, etc.)
├── schema.cs                  # Connector schema – object types and their attributes
├── import.cs                  # Full and delta import operations
├── export.cs                  # Export operations
├── parameters.cs              # Config-parameter pages, validation, and in-memory cache
├── routing.cs                 # Dispatches GetConfigParameters/ValidateConfigParameters
│                              #   to the correct helper method in parameters.cs
├── Connection.cs              # OpenConnection / CloseConnection helpers
├── constants.cs               # String and integer constants (parameter names, page sizes)
├── password.cs                # Password-sync operations (PCNS – change & set)
├── hierarchy.cs               # Hierarchical naming (LDAP-style GetHierarchy)
└── partitions.cs              # Partition enumeration (GetPartitions)
```

All source files declare the same `partial class EzmaExtension` inside the `FimSync_Ezma` namespace, so the compiler merges them into a single class.

---

## Feature Flags (Conditional Compilation Symbols)

The project uses `#if` blocks controlled by build-time symbols. Enable or disable each feature from **Project Properties → Build → Conditional compilation symbols**.

| Symbol | What it enables |
|---|---|
| `SUPPORT_IMPORT` | Full-import pipeline (`OpenImportConnection`, `GetImportEntries`, `CloseImportConnection`) |
| `SUPPORT_DELTA` | Delta-import path inside `GetImportEntries` (requires `SUPPORT_IMPORT`) |
| `SUPPORT_EXPORT` | Export pipeline (`OpenExportConnection`, `PutExportEntries`, `CloseExportConnection`) |
| `SUPPORT_PASSWORDS` | Password-sync via PCNS (`OpenPasswordConnection`, `ChangePassword`, `SetPassword`) |
| `USE_HIERARCHY` | LDAP-style hierarchical object naming (`GetHierarchy`) |
| `USE_PARTITIONS` | Partition support (`GetPartitions`) |
| `ADVANCED_PARAMETERS` | Multi-page schema UI (`IMAExtensible2GetParametersEx` instead of the standard interface) |

The default Debug configuration enables all symbols. For production connectors it is typical to keep only `SUPPORT_IMPORT` and `SUPPORT_EXPORT` (plus `SUPPORT_DELTA` if needed) and remove the rest.

---

## Code Organisation

### `capabilities.cs` — class entry point
Declares `EzmaExtension` and the ECMA2 interfaces it implements (conditional on the symbols above). Also exposes the `MACapabilities` property that tells MIM how the connector behaves (export type, DN style, concurrency, etc.).

### `schema.cs` — connector schema
Implements `IMAExtensible2GetSchema.GetSchema`. Opens a connection, builds `SchemaType` objects with their anchor and attribute definitions, and returns the assembled `Schema`.

### `import.cs` — import pipeline
Implements `IMAExtensible2CallImport`. Stores a connection in `PersistedConnector`, manages the watermark (custom data) for incremental imports, and routes to `FetchImport` or `FetchDeltaImport` based on the run-step type.

### `export.cs` — export pipeline
Implements `IMAExtensible2CallExport`. Stores a connection in `PersistedConnector` and provides a `PutExportEntries` stub for writing `CSEntryChange` objects back to the target system.

### `parameters.cs` — configuration UI and state
Builds the MIM configuration pages (Connectivity, Capabilities, Global, Partition, RunStep, Schema) and validates user input. Also owns the in-memory parameter cache (`Parameters`) accessed throughout the connector via `GetParameter()`.

### `routing.cs` — parameter dispatch
Thin routing layer that delegates `GetConfigParameters` / `ValidateConfigParameters` calls to the appropriate helper in `parameters.cs`. Handles both standard and `ADVANCED_PARAMETERS` variants.

### `Connection.cs` — connection management
`OpenConnection` reads credentials from the parameter store and returns a connection object (to be replaced with the real client for your target system). `CloseConnection` disposes the connection and saves any watermarks.

### `constants.cs` — shared constants
Centralises parameter-name strings (`"User Name"`, `"Password"`, `"Server Name"`) and page-size limits so they stay consistent across all files.

### `password.cs` — PCNS password sync
Implements `IMAExtensible2Password`: `OpenPasswordConnection` (separate from the regular connector to avoid PCNS/sync-cycle conflicts), `GetConnectionSecurityLevel`, `ChangePassword`, and `SetPassword`.

### `hierarchy.cs` — LDAP hierarchy
Stub implementation of `GetHierarchy` for connectors that use LDAP-style distinguished names.

### `partitions.cs` — partition enumeration
Stub implementation of `GetPartitions` for connectors that segment their namespace into partitions.

---

## Getting Started

1. **Clone or fork** this repository.
2. Open `ECMA2Template.sln` in Visual Studio.
3. In **Project Properties → Build**, adjust the conditional compilation symbols to match the features your target system requires.
4. Fill in `OpenConnection` / `CloseConnection` in `Connection.cs` with the real client for your target system.
5. Update `GetSchema` in `schema.cs` to reflect the object types and attributes in your target system.
6. Implement `FetchImport` (and optionally `FetchDeltaImport`) in `import.cs`.
7. Implement `PutExportEntries` in `export.cs` (if exporting).
8. Customise the configuration pages in `parameters.cs` and the constants in `constants.cs`.
9. Build the project – the output DLL is placed directly into `C:\Program Files\Microsoft Forefront Identity Manager\2010\Synchronization Service\Extensions`.
10. Create or update the Management Agent in MIM Synchronization Service Manager to reference `ECMA2Template.dll`.

---

## Logging

NLog is pre-configured via `LogManager.GetCurrentClassLogger()`. Add an `NLog.config` file to the MIM Extensions folder to control log targets and levels for your connector.

---

## References

- [The Undocumented Sync Engine](http://www.theundocumentedsyncengine.com/content/home.html) – the community resource referenced throughout the template comments
- [Microsoft Identity Manager documentation](https://docs.microsoft.com/en-us/microsoft-identity-manager/)
- [ECMA2 Connector development guide](https://docs.microsoft.com/en-us/microsoft-identity-manager/reference/microsoft-identity-manager-2016-connector-genericldap)
