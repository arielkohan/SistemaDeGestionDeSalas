import { Component, OnInit, Input } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';
import { Reserva } from '../../shared/model/Reserva';

@Component({
  selector: 'app-detalle-reserva',
  templateUrl: './detalle-reserva.component.html',
  styleUrls: ['./detalle-reserva.component.scss']
})
export class DetalleReservaComponent implements OnInit {

  public reserva: Reserva;

  constructor(public dialogRef: MdDialogRef<DetalleReservaComponent>) { }

  ngOnInit() {
  }


}