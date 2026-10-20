using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Imazen.WebP;
using MeloongCore;
using MeloongCore.Extensions;
using Microsoft.VisualBasic.CompilerServices;
using PCL.My;

namespace PCL;

public class MyBitmap
{
	private class WebPDecoder
	{
		public static Bitmap DecodeFromBytes(byte[] Bytes)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return new SimpleDecoder().DecodeFromBytes(Bytes, (long)Bytes.Length);
		}
	}

	public Bitmap Pic;

	[SpecialName]
	private ConcurrentDictionary<string, MyBitmap> _0024STATIC_0024_002Ector_00242011E_0024Cache;

	[SpecialName]
	private StaticLocalInitFlag _0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init;

	public static implicit operator MyBitmap(Image Image)
	{
		if (Image == null)
		{
			return null;
		}
		return new MyBitmap(Image);
	}

	public static implicit operator Image(MyBitmap Image)
	{
		return Image?.Pic;
	}

	public static implicit operator MyBitmap(ImageSource Image)
	{
		if (Image == null)
		{
			return null;
		}
		return new MyBitmap(Image);
	}

	public static implicit operator ImageSource(MyBitmap Image)
	{
		if (Image == null)
		{
			return null;
		}
		Bitmap pic = Image.Pic;
		Rectangle rect = new Rectangle(0, 0, pic.Width, pic.Height);
		BitmapData bitmapData = pic.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		try
		{
			BitmapSource bitmapSource = BitmapSource.Create(pic.Width, pic.Height, pic.HorizontalResolution, pic.VerticalResolution, PixelFormats.Bgra32, null, bitmapData.Scan0, checked(rect.Width * rect.Height * 4), bitmapData.Stride);
			bitmapSource.Freeze();
			return bitmapSource;
		}
		finally
		{
			pic.UnlockBits(bitmapData);
		}
	}

	public static implicit operator MyBitmap(Bitmap Image)
	{
		if (Image == null)
		{
			return null;
		}
		return new MyBitmap(Image);
	}

	public static implicit operator Bitmap(MyBitmap Image)
	{
		return Image?.Pic;
	}

	public static implicit operator MyBitmap(ImageBrush Image)
	{
		if (Image == null)
		{
			return null;
		}
		return new MyBitmap(Image);
	}

	public static implicit operator ImageBrush(MyBitmap Image)
	{
		if (Image == null)
		{
			return null;
		}
		return new ImageBrush(new MyBitmap(Image.Pic));
	}

	public MyBitmap()
	{
	}

	public MyBitmap(string FilePathOrResourceName)
	{
		try
		{
			FilePathOrResourceName = FilePathOrResourceName.Replace("pack://application:,,,/images/", ModBase.PathImage);
			if (StringExtensions.StartsWithF(FilePathOrResourceName, ModBase.PathImage, false))
			{
				if (_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init == null)
				{
					Interlocked.CompareExchange(ref _0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init, new StaticLocalInitFlag(), null);
				}
				bool lockTaken = false;
				try
				{
					Monitor.Enter(_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init, ref lockTaken);
					if (_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init.State == 0)
					{
						_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init.State = 2;
						_0024STATIC_0024_002Ector_00242011E_0024Cache = new ConcurrentDictionary<string, MyBitmap>();
					}
					else if (_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init.State == 2)
					{
						throw new IncompleteInitialization();
					}
				}
				finally
				{
					_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init.State = 1;
					if (lockTaken)
					{
						Monitor.Exit(_0024STATIC_0024_002Ector_00242011E_0024Cache_0024Init);
					}
				}
				if (_0024STATIC_0024_002Ector_00242011E_0024Cache.ContainsKey(FilePathOrResourceName))
				{
					Pic = _0024STATIC_0024_002Ector_00242011E_0024Cache[FilePathOrResourceName].Pic;
					return;
				}
				Pic = new MyBitmap((ImageSource)new ImageSourceConverter().ConvertFromString(FilePathOrResourceName));
				_0024STATIC_0024_002Ector_00242011E_0024Cache.TryAdd(FilePathOrResourceName, Pic);
				return;
			}
			using Stream stream = FileUtils.ReadAsStream(FilePathOrResourceName, (Type)null);
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			stream.Seek(0L, SeekOrigin.Begin);
			if (array[0] == 82 && array[1] == 73)
			{
				byte[] array2 = new byte[checked((int)(stream.Length - 1) + 1)];
				stream.Read(array2, 0, array2.Length);
				Pic = WebPDecoder.DecodeFromBytes(array2);
			}
			else
			{
				Pic = new Bitmap(stream);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Pic = (Bitmap)MyWpfExtension.Application.TryFindResource(FilePathOrResourceName);
			if (Pic == null)
			{
				Pic = new Bitmap(1, 1);
				if (ex2 is ArgumentException)
				{
					throw new Exception($"图片格式不支持，或图片文件损坏（{FilePathOrResourceName}）", ex2);
				}
				throw new Exception($"加载 MyBitmap 意外失败（{FilePathOrResourceName}）", ex2);
			}
			Logger.Warn(ex2, $"指定类型有误的 MyBitmap 加载（{FilePathOrResourceName}）", (LogBehavior)1, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\MyBitmap.vb");
			ProjectData.ClearProjectError();
		}
	}

	public MyBitmap(ImageSource Image)
	{
		using MemoryStream stream = new MemoryStream();
		PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
		pngBitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image));
		pngBitmapEncoder.Save(stream);
		Pic = new Bitmap(stream);
	}

	public MyBitmap(Image Image)
	{
		Pic = (Bitmap)Image;
	}

	public MyBitmap(Bitmap Image)
	{
		Pic = Image;
	}

	public MyBitmap(ImageBrush Image)
	{
		using MemoryStream stream = new MemoryStream();
		BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
		bmpBitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image.ImageSource));
		bmpBitmapEncoder.Save(stream);
		Pic = new Bitmap(stream);
	}

	public MyBitmap Clip(int X, int Y, int Width, int Height)
	{
		Bitmap bitmap = new Bitmap(Width, Height, Pic.PixelFormat);
		bitmap.SetResolution(Pic.HorizontalResolution, Pic.VerticalResolution);
		checked
		{
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				graphics.TranslateTransform(-X, -Y);
				graphics.DrawImage(Pic, new Rectangle(0, 0, Pic.Width, Pic.Height));
			}
			return bitmap;
		}
	}

	public MyBitmap RotateFlip(RotateFlipType Type)
	{
		Bitmap bitmap = new Bitmap(Pic);
		bitmap.SetResolution(Pic.HorizontalResolution, Pic.VerticalResolution);
		bitmap.RotateFlip(Type);
		return bitmap;
	}

	public void Save(string FilePath)
	{
		BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
		bitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)(ImageSource)this));
		using FileStream stream = FileUtils.CreateAsStream(FilePath);
		bitmapEncoder.Save(stream);
	}
}
