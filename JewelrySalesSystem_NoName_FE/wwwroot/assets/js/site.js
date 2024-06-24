// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Write your JavaScript code.
$(() => {
    LoadProdData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();
    connection.on("LoadProducts", function () {
        LoadProdData();
    })
    LoadProdData();
    function LoadProdData() {
        var tr = '';
        $.ajax({
            url: '/Products/GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td>${v.proId}</td>
                        <td>${v.proName}</td>
                        <td>${v.category}</td>
                        <td>${v.unitPrice}</td>
                        <td>${v.stockQty}</td>
                        <td>
                            <a href="../Products/Edit?id=${v.proId}">Edit</a>|
                            <a href="../Products/Details?id=${v.proId}">Details</a>|
                            <a href="../Products/Delete?id=${v.proId}">Delete</a>
                        </td>
                        </tr>`;
                    
                })
                $("#tableBody").html(tr)
               

                console.log(result)
                console.log(tr);
            },
            error: (error) => {
                console.log(error)
            }

        })
    }
})