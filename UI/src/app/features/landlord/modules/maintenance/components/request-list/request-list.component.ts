import { Component } from '@angular/core';

@Component({
  selector: 'app-request-list',
  standalone: true,
  template: `
    <div class="bg-white rounded-lg shadow">
      <div class="p-6">
        <div class="flex items-center justify-between mb-4">
          <div class="relative">
            <span class="material-icons absolute left-3 top-2 text-gray-400">search</span>
            <input 
              type="text"
              placeholder="Search requests"
              class="pl-10 pr-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
          </div>
          <div class="flex gap-2">
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">filter_list</span>
            </button>
            <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
              <span class="material-icons">download</span>
            </button>
          </div>
        </div>

        <table class="w-full">
          <thead>
            <tr class="border-b">
              <th class="text-left py-3 px-4">ID</th>
              <th class="text-left py-3 px-4">Property</th>
              <th class="text-left py-3 px-4">Issue</th>
              <th class="text-left py-3 px-4">Status</th>
              <th class="text-left py-3 px-4">Priority</th>
              <th class="text-left py-3 px-4">Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr class="border-b">
              <td class="py-3 px-4">#1234</td>
              <td class="py-3 px-4">Sunset Apartments</td>
              <td class="py-3 px-4">Plumbing issue</td>
              <td class="py-3 px-4">
                <span class="bg-yellow-100 text-yellow-800 px-2 py-1 rounded-full text-sm">
                  In Progress
                </span>
              </td>
              <td class="py-3 px-4">
                <span class="bg-red-100 text-red-800 px-2 py-1 rounded-full text-sm">
                  High
                </span>
              </td>
              <td class="py-3 px-4">
                <button class="text-gray-600 hover:text-gray-900">
                  <span class="material-icons">more_vert</span>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  `
})
export class RequestListComponent {}