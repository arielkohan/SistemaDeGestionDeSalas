export class Sala{
    salaID: number;
    nombre: string;
    ubicacion: string;
    tipoSalaID: number;
    capacidad: number;
    tipoSala: {
        tipoID: number,
        descripcion: string;    
    };

    constructor(){};
    // constructor(SalaID: number, Nombre: string, Ubicacion: string, TipoSalaId: number, TipoSala: any){
    // 	this.SalaID = SalaID;
    // 	this.Nombre = Nombre;
    // 	this.Ubicacion = Ubicacion;
    // 	this.TipoSalaID = TipoSalaId;
    // 	this.TipoSala = TipoSala;    	
    // };

    
}