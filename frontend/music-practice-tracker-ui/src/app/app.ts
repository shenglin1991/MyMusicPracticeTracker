import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly navigationItems = [
    { label: 'Dashboard', path: '/dashboard' },
    { label: 'Instruments', path: '/instruments' },
    { label: 'Sessions', path: '/sessions' },
    { label: 'Goals', path: '/settings' },
  ];
}
