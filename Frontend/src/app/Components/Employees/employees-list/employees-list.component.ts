import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/Models/employee.model';
import { EmployeeMain } from 'src/app/Models/employeemain.model';
import { EmployeeService } from 'src/app/Services/employee.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit {
  
  employee: Employee[] = [];
  constructor(private employeeService:EmployeeService){}
ngOnInit(): void {
    this.employeeService.getAllEmployee().subscribe(
      {
        next:(employee)=>{this.employee = employee.data},
        //next:(employee)=>{console.log(employee)},
        error:(response)=>{console.log(response)}
      }
    )

  } 

  /*constructor(private route:ActivatedRoute,private employeeService:EmployeeService,private router:Router) { }

  ngOnInit(): void {
     this.route.paramMap.subscribe(
      {
        next:(params)=>{
          console.log(params);
          const pg = params.get('2');
          if(pg){
              this.employeeService.getAllEmployee().subscribe(
                {
                  //next:(employee)=>{this.employee = employee.data},
                  next:(employee)=>{console.log(employee.data)},
                  error:(response)=>{console.log(response)}
                }, 
              );
          }
          else{
            console.log("Nooo")
          }
        }, 

 
    )*/

}
