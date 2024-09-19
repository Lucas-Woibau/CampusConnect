
document.addEventListener('DOMContentLoaded', function () {
    // Dados para o gráfico de instituições
    var instituicaoCounts = JSON.parse('@Html.Raw(ViewBag.UsersInst)');
    console.log('Dados de Instituições:', instituicaoCounts); // Verifique os dados

    // Ordenar e preparar os dados para o gráfico de instituições
    instituicaoCounts.sort((a, b) => b.Count - a.Count);
    var labelsInst = instituicaoCounts.map(x => x.Instituicao);
    var dataInst = instituicaoCounts.map(x => x.Count);

    // Definir cores fixas para instituições conhecidas
    var fixedColorsInst = {
        'UNESC': '#0000FF', // Azul
        'CASTELO BRANCO': '#00FF00', // Verde
        'IFES': '#FF0000', // Vermelho
        'SENAI': '#FFEB00' // Amarelo
    };

    // Função para gerar uma cor aleatória
    function generateRandomColor() {
        return '#' + Math.floor(Math.random() * 16777215).toString(16);
    }

    // Mapear instituições para cores
    var backgroundColorsInst = instituicaoCounts.map(x => {
        return fixedColorsInst[x.Instituicao] || generateRandomColor();
    });

    var ctxInst = document.getElementById('instituicaoChart').getContext('2d');
    var myPieChartInst = new Chart(ctxInst, {
        type: 'pie',
        data: {
            labels: labelsInst,
            datasets: [{
                data: dataInst,
                backgroundColor: backgroundColorsInst,
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'left',
                    labels: {
                        color: 'white',
                        font: {
                            size: 12 // Tamanho da fonte (opcional)
                        }
                    }
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            return 'Total de alunos: ' + tooltipItem.raw + '.';
                        }
                    }
                }
            }
        }
    });

    // Dados para o gráfico de rotas
    var rotaCounts = JSON.parse('@Html.Raw(ViewBag.Rotas)'); // Certifique-se de que ViewBag.Rotas contém os dados para rotas
    console.log('Dados de Rotas:', rotaCounts); // Verifique os dados

    // Ordenar e preparar os dados para o gráfico de rotas
    rotaCounts.sort((a, b) => b.Count - a.Count);
    var labelsRota = rotaCounts.map(x => x.Rota);
    var dataRota = rotaCounts.map(x => x.Count);

    // Definir cores fixas para rotas conhecidas
    var fixedColorsRota = {
        'UNESC': '#0000FF', // Azul
        'CASTELO/CENTRO/IFES': '#00FF00' // Verde
    };

    // Mapear rotas para cores
    var backgroundColorsRota = rotaCounts.map(x => {
        return fixedColorsRota[x.Rota] || generateRandomColor();
    });

    var ctxRota = document.getElementById('rotaChart').getContext('2d');
    var myPieChartRota = new Chart(ctxRota, {
        type: 'pie',
        data: {
            labels: labelsRota,
            datasets: [{
                data: dataRota,
                backgroundColor: backgroundColorsRota,
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'left',
                    labels: {
                        color: 'white',
                        font: {
                            size: 12 // Tamanho da fonte (opcional)
                        }
                    }
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            return 'Total: ' + tooltipItem.raw + '.';
                        }
                    }
                }
            }
        }
    });

});

// Atualizar o gráfico no redimensionamento da janela
window.addEventListener('resize', function () {
    ['instituicaoChart', 'rotaChart'].forEach(function(chartId) {
        var ctx = document.getElementById(chartId).getContext('2d');
        var myPieChart = Chart.getChart(ctx); // Obtém a instância do gráfico
        if (myPieChart) { // Verifica se o gráfico existe
            myPieChart.resize();
            myPieChart.update(); // Força a atualização do gráfico
        }
    });
});
