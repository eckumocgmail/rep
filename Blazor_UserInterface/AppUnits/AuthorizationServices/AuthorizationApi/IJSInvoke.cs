using Microsoft.JSInterop;

public interface IJSInvoke
{
    public Task<T> Eval<T>(string js);
    public Task<T> EvalAsync<T>(string js);
}

public class JSInvoke : IJSInvoke
{
    private IJSRuntime _jsr;
    private ILogger<JSInvoke> _logger;

    public JSInvoke(
            ILogger<JSInvoke> logger,
            IJSRuntime JSR)
    {
        this._jsr = JSR;
        this._logger = logger;
    }

    protected void Info(string message)
    {
        _logger.LogInformation(message);
    }

    

    public async Task<T> Eval<T>(string js)
    {
        return await _jsr.InvokeAsync<T>("eval", @"(function () {try{" + js + @"} catch (e) { alert('Ошибка при разборе JSON: ' + e); } })()");
    }

    public async Task<T> EvalAsync<T>(string js)
    {
        return await _jsr.InvokeAsync<T>("eval", @"(function () { try {" + js + @" } catch (e) { alert('Ошибка при разборе JSON: ' + e); }  })()");
    }
}
