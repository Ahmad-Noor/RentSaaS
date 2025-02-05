import { Component } from '@angular/core';

@Component({
  selector: 'app-resource-overview',
  standalone: true,
  template: `
    <div class="grid grid-cols-4 gap-4">
      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Resource Group</h3>
          <button class="text-[#0078D4] text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Active Resources: 12</span>
          </div>
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-yellow-500 rounded-full mr-2"></span>
            <span>Pending: 3</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Properties</h3>
          <button class="text-[#0078D4] text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Active: 8</span>
          </div>
          <div class="flex items-center text-sm">
            <span>Occupancy: 92%</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Tenants</h3>
          <button class="text-[#0078D4] text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Active: 45</span>
          </div>
          <div class="flex items-center text-sm">
            <span>New This Month: 3</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Maintenance</h3>
          <button class="text-[#0078D4] text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-yellow-500 rounded-full mr-2"></span>
            <span>Open Requests: 5</span>
          </div>
          <div class="flex items-center text-sm">
            <span>Completed: 28</span>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ResourceOverviewComponent {}