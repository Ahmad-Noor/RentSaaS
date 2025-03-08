import { Routes } from "@angular/router";
import { AddPropertyPage } from "./add-property/add-property.page";

export default [
  {
    path: "",
    loadComponent: () =>
      import("./properties-list/properties-list.page").then(
        (m) => m.PropertiesListPage
      ),
  },
  {
    path: "new",
    loadComponent: () =>
      import("./add-property/add-property.page").then((m) => m.AddPropertyPage),
  },
  {
    path: "edit/:id",
    loadComponent: () =>
      import("./add-property/add-property.page").then((m) => m.AddPropertyPage),
  },


  {
    path: "advertising",
    loadComponent: () =>
      import("./advertising/advertising.page").then((m) => m.AdvertisingPage),
  },
  {
    path: "advertising/create",
    loadComponent: () =>
      import("./create-listing/create-listing.page").then(
        (m) => m.CreateListingPage
      ),
  },
  // {
  //   path: "applications",
  //   loadComponent: () =>
  //     import("./applications/list/applications.page").then(
  //       (m) => m.ApplicationsPage
  //     ),
  // },
  // {
  //   path: "applications/send",
  //   loadComponent: () =>
  //     import("./applications/application-add-edit/application-add-edit.component").then(
  //       (m) => m.ApplicationAddEditComponent
  //     ),
  // },

  // {
  //   path: "applications/send/:id", // إضافة :id لاستقبال المعامل
  //   loadComponent: () =>
  //     import("./applications/application-add-edit/application-add-edit.component").then((m) => m.ApplicationAddEditComponent),
  // },

  {
    path: 'applications/application',
    loadComponent: () => import('./applications/application-add-edit/application-add-edit.component').then(m => m.ApplicationAddEditComponent)
  },
  {
    path: 'applications',
    loadComponent: () => import('./applications/list/applications.page').then(m => m.ApplicationsPage)
  },
  {
    path: "lease",
    loadComponent: () => import("./lease/list/lease.page").then((m) => m.LeasePage),
  },
  {
    path: "lease/lease-add-edit",
    loadComponent: () =>
      import("./lease/lease-add-edit/lease-add-edit.component").then((m) => m.LeaseAddEditComponent),
  },
  {


    path: "lease/lease-add-edit/:id", // إضافة :id لاستقبال المعامل
    loadComponent: () =>
      import("./lease/lease-add-edit/lease-add-edit.component").then((m) => m.LeaseAddEditComponent),
  }
] as Routes;
