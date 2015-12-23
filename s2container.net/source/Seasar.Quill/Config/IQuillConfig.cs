﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Quill.Config {
    public interface IQuillConfig {
        XElement BaseElement { get; }

        string GetValue(string nodePath);

        string GetValue(string nodePath, Func<XElement, bool> isTarget);

        List<string> GetValues(string nodePath);

        List<string> GetValues(string nodePath, Func<XElement, bool> isTarget);

        XElement GetElement(string nodePath);

        XElement GetElement(string nodePath, Func<XElement, bool> isTarget);
    }
}
