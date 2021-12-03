import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OauthCodeComponent } from './oauth-code.component';

describe('OauthCodeComponent', () => {
  let component: OauthCodeComponent;
  let fixture: ComponentFixture<OauthCodeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OauthCodeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OauthCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
