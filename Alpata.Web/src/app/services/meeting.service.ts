import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from '../models/meeting';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  private apiUrl = 'https://localhost:44373/api/meetings';

  constructor(private http: HttpClient) { }

  getMeetings(): Observable<Meeting[]> {
    return this.http.get<Meeting[]>(this.apiUrl);
  }

  getMeeting(id: number): Observable<Meeting> {
    return this.http.get<Meeting>(`${this.apiUrl}/${id}`);
  }

  createMeeting(meeting: Meeting): Observable<Meeting> {
    return this.http.post<Meeting>(this.apiUrl, meeting);
  }

  updateMeeting(id: number, meeting: Meeting): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, meeting);
  }

  deleteMeeting(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
