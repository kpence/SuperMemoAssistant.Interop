﻿using System.Drawing;
using SuperMemoAssistant.Interop.SuperMemo.Components.Models;
using SuperMemoAssistant.Interop.SuperMemo.Registry.Members;

namespace SuperMemoAssistant.Interop.SuperMemo.Components.Types
{
  public interface IComponentSound : IComponent
  {
    ISound Sound { get; }
    AtFlags PlayAt { get; }
    uint ExtractStart { get; }
    uint ExtractStop { get; }
    bool IsContinuous { get; }
    MediaPanelType Panel { get; }
    Color Color { get; }
    TextAlignment TextAlignment { get; }
  }
}
