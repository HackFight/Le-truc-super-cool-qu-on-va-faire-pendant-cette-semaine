// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
namespace InControl.NativeDeviceProfiles
{
	// @cond nodoc
	[Preserve, NativeInputDeviceProfile]
	public class DualSenseMFiNativeProfile : InputDeviceProfile
	{
		public override void Define()
		{
			base.Define();

			DeviceName = "Sony DualSense Controller";
			DeviceNotes = "Sony DualSense Controller on iOS / tvOS / macOS";

			DeviceClass = InputDeviceClass.Controller;
			DeviceStyle = InputDeviceStyle.PlayStation4;

			IncludePlatforms = new[]
			{
				"iOS",
				"tvOS",
				"iPhone",
				"iPad",
				"AppleTV",
				"OS X",
			};

			Matchers = new[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.AppleGameController,
					VendorID = 0xFFFF,
					ProductID = 0x0003,
					VersionNumber = 0
				}
			};

			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Name = "Cross",
					Target = InputControlType.Action1,
					Source = Button( 0 )
				},
				new InputControlMapping
				{
					Name = "Circle",
					Target = InputControlType.Action2,
					Source = Button( 1 )
				},
				new InputControlMapping
				{
					Name = "Square",
					Target = InputControlType.Action3,
					Source = Button( 2 )
				},
				new InputControlMapping
				{
					Name = "Triangle",
					Target = InputControlType.Action4,
					Source = Button( 3 )
				},
				new InputControlMapping
				{
					Name = "L1",
					Target = InputControlType.LeftBumper,
					Source = Button( 4 )
				},
				new InputControlMapping
				{
					Name = "R1",
					Target = InputControlType.RightBumper,
					Source = Button( 5 )
				},
				new InputControlMapping
				{
					Name = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = Button( 6 )
				},
				new InputControlMapping
				{
					Name = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = Button( 7 )
				},
				new InputControlMapping
				{
					Name = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = Button( 8 )
				},
				new InputControlMapping
				{
					Name = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = Button( 9 )
				},
				new InputControlMapping
				{
					Name = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = Button( 10 )
				},
				new InputControlMapping
				{
					Name = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = Button( 11 )
				},
				new InputControlMapping
				{
					Name = "Create",
					Target = InputControlType.Create,
					Source = Button( 13 )
				},
				new InputControlMapping
				{
					Name = "Options",
					Target = InputControlType.Options,
					Source = Button( 12 )
				},
				new InputControlMapping
				{
					Name = "System",
					Target = InputControlType.System,
					Source = Button( 14 )
				}
			};

			AnalogMappings = new[]
			{
				LeftStickLeftMapping( 0 ),
				LeftStickRightMapping( 0 ),
				LeftStickUpMapping2( 1 ),
				LeftStickDownMapping2( 1 ),

				RightStickLeftMapping( 2 ),
				RightStickRightMapping( 2 ),
				RightStickUpMapping2( 3 ),
				RightStickDownMapping2( 3 ),

				new InputControlMapping
				{
					Name = "L2",
					Target = InputControlType.LeftTrigger,
					Source = Analog( 4 ),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne,
				},
				new InputControlMapping
				{
					Name = "R2",
					Target = InputControlType.RightTrigger,
					Source = Analog( 5 ),
					SourceRange = InputRangeType.ZeroToOne,
					TargetRange = InputRangeType.ZeroToOne,
				}
			};
		}
	}

	// @endcond
}
