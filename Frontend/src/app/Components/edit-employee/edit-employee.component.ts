import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Employee } from '../../Models/employee.model';
import { EmployeeService } from '../../Services/employee.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {
  employeeDetails:Employee = {
    id:"",
    email:"",
    first_name:"",
    last_name:"",
    avatar:"",
  };

  constructor(private route:ActivatedRoute,private employeeService:EmployeeService,private router:Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      {
        next:(params)=>{
          const id = params.get('id');
          if(id){
              this.employeeService.getEmployee(id).subscribe(
                {
                next:(response)=>{
                this.employeeDetails = response;
                }, 
                error:(response)=>{console.log(response)}
              }
              );
          }
          else{
            console.log("Nooo")
          }
        },
      }
    )
  }

  updateEmployee(){
    this.employeeService.updateEmployee(this.employeeDetails.id,this.employeeDetails).subscribe
    ({
      next:(response)=>{
        this.router.navigate(['employees']);
      }
    })

  }
  deleteEmployee(id:string){
    this.employeeService.deleteEmployee(id).subscribe
    ({
      next:(response)=>{
        this.router.navigate(['employees']);
      }
    })

  }
}
