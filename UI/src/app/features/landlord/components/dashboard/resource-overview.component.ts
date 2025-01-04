import { Component } from '@angular/core';

@Component({
  selector: 'app-resource-overview',
  standalone: true,
  template: `
    <div class="grid grid-cols-4 gap-4 mb-6">
      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Resource Group</h3>
          <button class="text-blue-600 text-sm">Edit</button>
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
          <h3 class="text-lg font-semibold">Web Front End</h3>
          <button class="text-blue-600 text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Status: Running</span>
          </div>
          <div class="flex items-center text-sm">
            <span>Response Time: 189.32ms</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Database</h3>
          <button class="text-blue-600 text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Status: Available</span>
          </div>
          <div class="flex items-center text-sm">
            <span>Size: 2.88 GB</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <div class="flex justify-between items-center mb-2">
          <h3 class="text-lg font-semibold">Processes</h3>
          <button class="text-blue-600 text-sm">Edit</button>
        </div>
        <div class="space-y-2">
          <div class="flex items-center text-sm">
            <span class="w-3 h-3 bg-green-500 rounded-full mr-2"></span>
            <span>Active: 27</span>
          </div>
          <div class="flex items-center text-sm">
            <span>Completed: 623</span>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ResourceOverviewComponent {}