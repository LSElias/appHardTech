////https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.2.2/Chart.bundle.min.js

function grafico(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    display: false, 
                     },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 10,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 14,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],


                }

            }
        });

}
function grafico1(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico1").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    labels: {
                        fontColor: "black",
                    },
                    display: true,
                },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,


            }


        });
}
function grafico2(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico2").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    labels: {
                    fontColor: "black",
                    },
                  display: true,
                },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,


                }

            
        });

}

function grafico3(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico3").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    display: false,
                },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 10,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 12    ,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],


                }

            }
        });

}

function grafico5(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico5").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    display: false,
                },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 10,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 12,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],


                }

            }
        });

}

function grafico6(titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data =
    {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx1 = document.getElementById("grafico6").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: tipo,
            data: data,
            options:
            {
                animation: {
                    duration: 1000 //tiempo de animación general
                },

                hover: {
                    animationDuration: 1000 //duración de las animaciones al pasar el cursor sobre un elemento
                },
                responsiveAnimationDuration: 3000, //duración de la animación después de un cambio de tamaño
                legend: {
                    display: false,
                },
                title:
                {
                    display: true,
                    text: tituloEtiquetas,
                    fontColor: "black"
                },
                responsive: true,
                maintainAspectRatio: true,
                scales: {
                    yAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 10,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: "black",
                            fontSize: 12,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }],


                }

            }
        });

}