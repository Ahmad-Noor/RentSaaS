import { Injectable } from '@angular/core';
import { MasterService } from '../../../../core/services/master/master.service';
import { environment } from '../../../../../environments/environment';
import { APIConstant } from '../../../../core/constants/APIConstant';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private mater:MasterService) { }
  getAllUsers(){
    this.mater.get(environment.apiURL + APIConstant.user.getAllUsers);
  }
}
