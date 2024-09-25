$('.close-alert').click(function () {
    $('.alert').hide(400);
});

$(document).ready(function () {
    GetDataTable("#TabelaAlunosNota");
    GetDataTable("#TabelaUsuarios");
    GetDataTable("#tabelaMateria");
    GetDataTable("#TabelaAlunoTurma");
    GetDataTable("#TabelaTurma");
    GetDataTable("#TabelaMateriaProfessor");

    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');
        console.log(usuarioId);


        $.ajax({
            type: 'GET',
            url: '/Usuario/ListarContatosPorUsuarioId/' + usuarioId,
            success: function (result) {
                $("#listaContatosUsuario").html(result);
                $('#modalContatosUsuario').modal(`show`);
                GetDataTable('#TabelaContatosUsuarios');
            }
        });


    });
});




function GetDataTable(id) {
    if ($.fn.dataTable.isDataTable(id)) {
        $(id).DataTable().destroy();
    }
    $(id).DataTable(
        {
            columnDefs: [{
                "defaultContent": "",
                "targets": "_all"
            }],
            responsive: true,
            "ordering": true,
            "paging": true,
            "searching": true,
            "oLanguage": {
                "sEmptyTable": "Nenhum registro encontrado na tabela",
                "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
                "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "Mostrar _MENU_ registros por pagina",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Proximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Ultimo"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                }
            }
        });
}


function togglePassword() {
    const passwordInput = document.getElementById('password');
    const toggleIcon = document.querySelector('.toggle-password');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        toggleIcon.textContent = '🙈'; // Muda o ícone quando a senha está visível
    } else {
        passwordInput.type = 'password';
        toggleIcon.textContent = '👁️'; // Muda o ícone quando a senha está oculta
    }
}

