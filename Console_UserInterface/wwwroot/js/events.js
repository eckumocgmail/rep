function selectMicrophone() {
    navigator.mediaDevices.enumerateDevices().then((devices) => {
        devices = devices.filter((d) => d.kind === 'audioinput');
    });
}



/* this defines the end of your file, whenever called, a new file is 
created from the recorded data */
function createFileFormCurrentRecordedData() {


    /* then upload oder directly download your file / blob depending on your needs */
}





var recordedData = null;
var mediaRecorder = null;

function startCamera(vid) {
    var video = document.getElementById(vid);
    if (!video)
        throw new { message: 'Не удалось выбрать элемент по ИД ' + vid }
    if (!navigator.mediaDevices)
        throw new { message: 'камера не поддерживается' }

    navigator.mediaDevices.getUserMedia({ video: true }).then((stream) => {
        recordedData = [];
        mediaRecorder = new MediaRecorder(stream, {});
        mediaRecorder.ondataavailable = (event) => {
            recordedData.push(event.data)
        }

        mediaRecorder.onstop = function () {
            console.info(recordedData);
            const blob = new Blob(recordedData, { type: "audio/ogg" });
            const file = new File([blob], "track.ogg", { type: "audio/ogg" });
            const reader = new FileReader();
        }
        video.srcObject = stream;
        video.play();
        mediaRecorder.start();
    });
}
function stopCamera() {
    var video = document.getElementById('vid');
    if (!video)
        throw new { message: 'Не удалось выбрать элемент по ИД ' + vid }
    if (!navigator.mediaDevices)
        throw new { message: 'камера не поддерживается' }
    navigator.mediaDevices.getUserMedia({ video: true }).then((stream) => {
        video.srcObject = stream;
        video.pause();
        mediaRecorder.stop();
    });
}
function infoRecord() {
    console.log(recordedData);
    console.log(mediaRecorder.requestData());
}