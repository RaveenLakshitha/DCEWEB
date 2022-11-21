import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/Models/employee.model';
import { EmployeeService } from 'src/app/Services/employee.service';
@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  addEmployeeRequest:Employee = {
    id:"",
    email:"",
    first_name:"",
    last_name:"",
    avatar:""
  }
  constructor(private employeeService:EmployeeService,private router:Router) { }

  ngOnInit(): void {
  }

  addEmployee(){
    this.employeeService.addEmployee(this.addEmployeeRequest).subscribe(
      {
        next:(employee)=>{
          this.router.navigate(['employees']);
        },
        error:(response)=>{console.log(response)}
      }
    )
  }


}
