import { Component, OnInit, isDevMode } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  constructor(){
  }
  
  ngOnInit(): void {
    if (!isDevMode()) {
      console.log('Production mode');
    }
  }
}
