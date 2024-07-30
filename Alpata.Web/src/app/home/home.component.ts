import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MeetingService } from '../services/meeting.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  meetingForm: FormGroup;

  constructor(private fb: FormBuilder, private meetingService: MeetingService) {
    this.meetingForm = this.fb.group({
      title: ['', Validators.required],
      date: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
      description: ['', Validators.maxLength(500)]
    });
  }

  ngOnInit(): void {}

  submitMeeting(): void {
    if (this.meetingForm.valid) {
      const meeting = this.meetingForm.value;
      meeting.startDate = new Date(meeting.date);
      meeting.endDate = new Date(meeting.date);
      meeting.startDate.setHours(this.getHours(meeting.startTime), this.getMinutes(meeting.startTime));
      meeting.endDate.setHours(this.getHours(meeting.endTime), this.getMinutes(meeting.endTime));
      
      this.meetingService.createMeeting(meeting).subscribe(
        response => {
          console.log('Toplantı başarıyla oluşturuldu', response);
        },
        error => {
          console.error('Toplantı oluşturulurken hata oluştu', error);
        }
      );
    }
  }

  private getHours(time: string): number {
    return parseInt(time.split(':')[0], 10);
  }

  private getMinutes(time: string): number {
    return parseInt(time.split(':')[1], 10);
  }
}
