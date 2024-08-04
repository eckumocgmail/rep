function showNotification( message )
{
    console.log(`showNotification( ${message} )`);
    let notifications_block = document.getElementById('notifications_block');
    if (!notifications_block) throw new Error('#notifications_block not found');
    let item = `<li class="list-group-item">${message}<li>`;
    var div = document.createElement('div');
    div.innerHTML = item;
    notifications_block.appendChild(div);
    return new Date().getTime();
}
function openDialog( text )
{
    $('.modal').modal({ dismiss: true });
    document.getElementById('#modal_block .modal-body').innerHTML = text;
}