import { Sala } from './Sala';
export class Reserva{

        reservaID: number;
        //FechaInicio y FechaFin deben compartir el dia y variar en la hora y min.
        fechaInicio: Date;
        fechaFin: Date;
        cantidadPersonas: number;
        motivo: string;
        servicio: boolean;
        almuerzo: boolean;
        proyector: boolean;
        
        salaID: number;
        sala: Sala;

        responsableID : number;
        //public virtual Empleado Responsable { get; set; }

        encuestaID : number;
        //public virtual Encuesta Encuesta  { get; set; }

        constructor() {                       
         }

}