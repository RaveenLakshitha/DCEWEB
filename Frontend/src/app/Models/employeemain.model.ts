import { Employee } from "./employee.model";

export interface EmployeeMain{
    data:Employee[];
    page:string;
    per_page:string;
    total:string;
    total_pages:string;
}