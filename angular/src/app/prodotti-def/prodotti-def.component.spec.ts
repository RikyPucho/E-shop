import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdottiDefComponent } from './prodotti-def.component';

describe('ProdottiDefComponent', () => {
  let component: ProdottiDefComponent;
  let fixture: ComponentFixture<ProdottiDefComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProdottiDefComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProdottiDefComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
