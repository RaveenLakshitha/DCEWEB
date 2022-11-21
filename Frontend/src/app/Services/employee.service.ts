import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Employee } from '../Models/employee.model';
import { Observable } from 'rxjs';
import { EmployeeMain } from '../Models/employeemain.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseApiUrl:string  = environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getAllEmployee():Observable<EmployeeMain>{
    return this.http.get<EmployeeMain>(this.baseApiUrl + 'api/users/');
  }

  addEmployee(addEmployeeRequest:Employee):Observable<Employee>{
    addEmployeeRequest.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<Employee>(
      this.baseApiUrl + 'api/Employees',
      addEmployeeRequest
    )
  }

  getEmployee(id:string):Observable<Employee>{
    return this.http.get<Employee>(this.baseApiUrl + 'api/users/' + id);
  }

  updateEmployee(id:string,updateEmployeeRequest: Employee):
  Observable<Employee>{
    return this.http.put<Employee>(this.baseApiUrl + 'api/users/' + id,
    updateEmployeeRequest);
  }

  deleteEmployee(id:string):Observable<Employee>{
    return this.http.delete<Employee>(this.baseApiUrl + 'api/users/' + id)

  }
}
