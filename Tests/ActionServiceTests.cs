﻿using CardActionsApi.Models;
using CardActionsApi.Providers;
using CardActionsApi.Services.Actions;
using CardActionsApi.Services.Card;
using CardActionsApi.SpecificationBuilder;
using Microsoft.Extensions.Logging;
using Moq;
using Specifications.Builders;

namespace Tests;

public class ActionServiceTests
{
    private readonly Mock<ICardService> _mockCardService;
    private readonly Mock<ISpecificationsProvider<CardDetails>> _mockSpecificationProvider;
    private IActionService _service;
    private Mock<ILogger<ActionService>> _mockLogger;
    public ActionServiceTests()
    {
        _mockCardService = new Mock<ICardService>();
        _mockSpecificationProvider = new Mock<ISpecificationsProvider<CardDetails>>();
        _mockLogger = new Mock<ILogger<ActionService>>();   
    }

    private void Create_SUT()
    {
        _service = new ActionService(_mockSpecificationProvider.Object, _mockCardService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetCardActions_ValidUserAndCard_ReturnsActions()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";
        var cardDetails = new CardDetails(cardNumber, It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());
        var dict = new Dictionary<int, ISpecificationBuilder<CardDetails>>();

        dict.Add(1, new ActionSpecificationBuilder());
        dict.Add(2, new ActionSpecificationBuilder());

        var parsedToActionNames = dict.Select(rule => $"ACTION{rule.Key}");

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(cardDetails);
        _mockSpecificationProvider.Setup(x => x.GetDefinitions()).Returns(dict); // Act
        Create_SUT();
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(parsedToActionNames, result.Value);
    }

    [Fact]
    public async Task GetCardActions_CardNotFound_ReturnsFailure()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync((CardDetails)null);
        Create_SUT();
        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("No card found", result.Errors);
    }

    [Fact]
    public async Task GetCardActions_EmptyActionsList_ReturnsEmptyList()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";
        var cardDetails = new CardDetails(cardNumber, It.IsAny<CardType>(), It.IsAny<CardStatus>(),
            It.IsAny<bool>());
        var expectedActions = new Dictionary<int, ISpecificationBuilder<CardDetails>>();

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ReturnsAsync(cardDetails);
        _mockSpecificationProvider.Setup(p => p.GetDefinitions())
            .Returns(expectedActions);
        Create_SUT();
        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetCardActions_ExceptionThrown_ReturnsFailure()
    {
        // Arrange
        var userId = "user123";
        var cardNumber = "1234567890";

        _mockCardService.Setup(s => s.GetCardDetails(userId, cardNumber))
            .ThrowsAsync(new Exception("Unexpected error"));
        Create_SUT();
        // Act
        var result = await _service.GetCardActions(userId, cardNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Unexpected error", result.Errors);
    }
}