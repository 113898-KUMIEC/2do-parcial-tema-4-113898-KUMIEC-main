


function actualizarAviones() {
    let txtModelo = document.getElementById('txtModelo');
    let txtCantMotores = document.getElementById('txtCantMotores');
    let txtCantAsientos = document.getElementById('txtCantAsientos');
    let txtFabricante = document.getElementById('txtFabricante');
    let txtDatosVarios = document.getElementById('txtDatosVarios');
  
    if (txtModelo.value === "") {
      alert("El modelo es obligatorio, hubo un problema");
      return false;
    }
    if (txtCantMotores.value === "") {
      alert("El txtCantMotores es obligatorio");
      return false;
    }
    if (txtCantAsientos.value === "") {
      alert("El txtCantAsientos es obligatorio");
      return false;
    }
    if (txtDatosVarios.value === "") {
        alert("El txtDatosVarios es obligatorio");
        return false;
    }
  
    const URL = 'https://localhost:7178/putAviones';
  
    const avionId = localStorage.getItem('avionId'); // Obtener el ID del avión desde el almacenamiento local

    const request = {
        id: avionId,
        cantidadAsientos: txtCantAsientos.value,
        modelo: txtModelo.value,
        cantidadMotores: txtCantMotores.value,
        datosVarios: txtDatosVarios.value
    };
  
    fetch(URL, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(request)
    })
      .then(respuesta => respuesta.json())
      .then(respuesta => {
        if (respuesta.ok) {
          alert('Avion agregado con éxito');
        } else {
          alert('ERROR');
        }
      })
      .catch(err => alert('ERROR: ' + err));
      
  }
  
  const URLAVIONES = 'https://localhost:7178/api/Avion/GetAviones'  

function MostrarAvion() {
    fetch(URLAVIONES)
    
        .then((respuesta) => respuesta.json())
        .then((respuesta) => {
            if (!respuesta.ok) {
                alert("ERROR!")
                return
            }
            let txtModelo = document.getElementById('txtModelo');
            let txtCantMotores = document.getElementById('txtCantMotores');
            let txtCantAsientos = document.getElementById('txtCantAsientos');
            let txtFabricante = document.getElementById('txtFabricante');
            let txtDatosVarios = document.getElementById('txtDatosVarios');
            

            const avion = respuesta.listAviones[0];
            
            localStorage.setItem('avionId', avion.id); 
            txtModelo.value = avion.modelo;
            txtCantMotores.value = avion.cantidadMotores;
            txtCantAsientos.value = avion.cantidadAsientos;
            txtFabricante.value = avion.fabricanteNavigation;
            txtDatosVarios.value = avion.datosVarios;
            


        }).catch((err)=>{
            alert("No funciono")
        })
}

window.addEventListener('DOMContentLoaded', () => {
    MostrarAvion();
  });