function openModalDialog(contentId)
{
    let pModalDialog = document.getElementById('modal_dialog');
    if (!pModalDialog) {
        let div = document.createElement('div');
        div.innerHTML = `
<div class="modal" id="modal_dialog" tabindex="-1">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Modal title</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body" id="modal_body_slot"></div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
<div id='modal_body'></div>
        `;
    }
    
    var modal_body = document.getElementById('modal_body');
    while (modal_body.childNodes.length > 0)
        modal_body.removeChild(modal_body.childNodes[modal_body.childNodes.length-1]);
    document.getElementById(contentId)
    $('#modal_dialog').modal({ toggle: true });
    return 1;
}