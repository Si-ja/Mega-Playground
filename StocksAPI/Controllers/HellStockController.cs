﻿using Data.Models.Stocks;
using DataAccess.DataHandler;
using DataAccess.Services.Connections;
using Microsoft.AspNetCore.Mvc;
using MonitoringTools.Prometheus;
using StocksAPI.Services.DataCollection;
using StocksAPI.Services.StocksRetrieval;

namespace StocksAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class HellStockController : ControllerBase
{
    private readonly ILogger<HellStockController> logger;
    private readonly IStocksDataRetriever stocksDataRetriever;
    private readonly IMonitoringMetrics monitoringMetrics;
    private readonly IUserDataCollector userDataCollector;
    private readonly IUserDataHandler userDataHandler;

    public HellStockController(
        ILogger<HellStockController> logger,
        IStocksDataRetriever stocksDataRetriever,
        IMonitoringMetrics monitoringMetrics,
        IUserDataCollector userDataCollector,
        IUserDataHandler userDataHandler)
    {
        this.logger = logger;
        this.stocksDataRetriever = stocksDataRetriever;
        this.monitoringMetrics = monitoringMetrics;
        this.userDataCollector = userDataCollector;
        this.userDataHandler = userDataHandler;
    }

    [HttpGet(Name = "GetHellStock")]
    public async Task<ActionResult<StockModel>> Get()
    {
        try
        {
            var userDataModel = await userDataCollector.GetUserDataAsync(Request);
            await userDataHandler.InsertUserData(userDataModel, DbConnectionList.Postgres);
            this.logger.LogInformation($"A request has been made to the {nameof(HellStockController)}.");
        } 
        catch (Exception e)
        {
            this.logger.LogError($"There was an issue loading and saving data while processing {nameof(HellStockController)} request." +
                $"Error message: {e}");
        }
        finally
        {
            this.monitoringMetrics.IncrementUserMadeRequest(nameof(HellStockController));            
        }

        var hellStock = await stocksDataRetriever.GetHellStockAsync();
        return hellStock;
    }
}
