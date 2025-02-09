import { Routes } from "@angular/router";
import { HomePage } from "./home/home.page";
import { authGuard } from "./guards/Auth/auth.guard";
import { UserAuthenticatedGuard } from "./guards/Auth/user-authenticated.guard";

export const routes: Routes = [
  {
    path: "",
    component: HomePage,
  },
  {
    path: "pricing",
    loadComponent: () =>
      import("./home/pricing/pages/pricing/pricing.page").then(
        (m) => m.PricingPage
      ),
  },
  {
    path: "login",
    loadComponent: () =>
      import("./auth/login/login.page").then((m) => m.LoginPage),
    canActivate:[UserAuthenticatedGuard]

  },
  {
    path: "register",
    loadComponent: () =>
      import("./auth/register/register.page").then((m) => m.RegisterPage),
    canActivate:[UserAuthenticatedGuard]
  },
  {
    path: "landlord",
    loadChildren: () =>
      import("./landlord/landlord.routes").then((m) => m.default),
    canActivate:[authGuard]
  },
  {
    path: "tenant",
    loadChildren: () => import("./tenant/tenant.routes"),
    canActivate:[authGuard]
  },
  {
    path: "**",
    redirectTo: "",
  },
];
