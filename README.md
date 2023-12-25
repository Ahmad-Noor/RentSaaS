# RentSaaS

```mermaid
sequenceDiagram
    participant request
    participant middleware
    participant endpoint    
    request->>middleware: /?tenant=SkyRealty
    middleware->>endpoint: TenantService.Set("SkyRealty")
    endpoint->>request: Filtered Query by Tenant
```