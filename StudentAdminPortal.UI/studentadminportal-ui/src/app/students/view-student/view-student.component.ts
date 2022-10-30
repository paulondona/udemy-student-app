import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { Student } from 'src/app/models/ui-models/student.model';
import { GenderService } from 'src/app/services/gender.service';
import { StudentService } from '../student.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {
  studentId: string | null | undefined;
  genderList: Gender[] = [];
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    profileImageUrl: '',
    genderId: '',
    gender: {
      id: '',
      description: '',
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: ''
    }
  };

  constructor(private readonly studentService: StudentService,
    private readonly route: ActivatedRoute,
    private readonly genderService: GenderService,
    private snackBar: MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (param) => {
        this.studentId = param.get('id');

        if(this.studentId)
        {
          this.studentService.getStudent(this.studentId).subscribe({
            next: (student) => {
             this.student = student;
            },
            error: (e) => {

            }
          });

          this.genderService.getGenderList().subscribe({
            next: (genders) => {
              this.genderList = genders;
            },
            error: (e) => {

            }
          });
        }
      }
    );

  }

  onUpdate(): void {
    // call student service to udpate student
    this.studentService.updateStudent(this.student.id, this.student).subscribe({
      next: (stundenResp) => {
        //Show Notification
        this.snackBar.open("Student updated successfully.", undefined, {
          duration: 2000,
        });
      },
      error: (e) => {

      }
    });
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id).subscribe({
      next: (studentResp) => {
        this.snackBar.open("Student deleted successfully.", undefined, {
          duration: 2000,
        });

        setTimeout(() => {
          this.router.navigateByUrl("students");
        },2000)
      },
      error : (e) => {

      }
    });
  }
}
