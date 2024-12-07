import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';   
import { MasterService } from '../../../core/services/master/master.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent  {

  ticketsArray: any[] = [];
  selectedProjectData: any ;
  status: string[]= ['To Do','In Progress','Done'];

  constructor(private master: MasterService, private http: HttpClient) {
    this.master.onProjectChange.subscribe((res: any) => {
      debugger;
      this.getProjectTickets(res.projectId);
      this.selectedProjectData = res;
    })
    this.master.onTicketCreate.subscribe((res: any) => {
      debugger;
      this.getProjectTickets(this.selectedProjectData.projectId);
    })
  }

  getProjectTickets(id: number) {
    this.http.get('https://freeapi.miniprojectideas.com/api/Jira/GetTicketsByProjectId?projectid=' + id).subscribe((res: any) => {
      this.ticketsArray = res.data;
    })
  }

  filterTicket(status: string) {
    return this.ticketsArray.filter(m=>m.status == status)
  }
}
